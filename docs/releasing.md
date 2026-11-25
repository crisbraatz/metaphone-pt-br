# Releasing

This guide describes how MetaphonePtBr is published to NuGet.org. Publishing uses [NuGet Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing) (OIDC), so no API key is stored in the repository or in GitHub secrets. The workflow is [`.github/workflows/release.yml`](../.github/workflows/release.yml).

NuGet package IDs and versions are permanent. A version that has been published cannot be replaced, so run the dry run and the local checks before pushing a tag.

## Prerequisites

- Write access to the `crisbraatz/metaphone-pt-br` repository.
- A nuget.org account that owns the `MetaphonePtBr` package.
- The .NET SDK pinned in [`global.json`](../global.json), if you want to pack locally.

## One-time setup

### 1. nuget.org Trusted Publishing policy

Go to <https://www.nuget.org/account/trustedpublishing> and select **Add policy** with these values:

| Field | Value |
|-------|-------|
| Repository Owner | `crisbraatz` |
| Repository | `metaphone-pt-br` |
| Workflow File | `release.yml` |
| Environment | `nuget` |

Notes:

- Workflow File takes the filename only, with no path. Matching is case-insensitive. Do not use the workflow `name:` field.
- The policy must name the environment, because the publish job declares `environment: nuget`.

### 2. GitHub environment `nuget`

In the repository, go to **Settings > Environments > New environment** and create `nuget`.

- Add an environment secret named `NUGET_USER`. Its value is your nuget.org profile name, not your email address.
- Optional: add **Required reviewers** as an approval gate before the publish job runs.
- Recommended: under **Deployment branches and tags**, restrict the environment to tags matching `v*`, so the environment cannot be used by other refs.

## Pre-release checks

1. Confirm `<Version>` in `Directory.Build.props` is the version you are releasing (for example `3.0.0`).
2. Confirm `CHANGELOG.md` has a dated section for that version and an empty `[Unreleased]` section on top.
3. Pack locally from the repository root:

   ```bash
   dotnet pack src/MetaphonePtBr -c Release -o artifacts
   ```

4. Inspect the `.nupkg` if needed. It is a zip archive: `unzip -l artifacts/MetaphonePtBr.<version>.nupkg`.

## Dry run

The dry run exercises the build, test and pack steps without publishing anything.

1. Open **Actions > Release > Run workflow**, select `main`, and run it.
2. Wait for the `build` job to finish. It restores, builds, tests and packs the project, then uploads the `.nupkg` as the `packages` artifact.
3. Download the `packages` artifact from the run summary and check the package.

The dry run never logs in to NuGet, never pushes a package and never creates a GitHub Release. The `publish` job is skipped unless the workflow was started by a `v*` tag push.

## Publish

1. Make sure the release commit is on `main` and that its `<Version>` matches the version you are releasing.
2. Create and push the tag from that commit:

   ```bash
   git tag v3.0.0
   git push origin v3.0.0
   ```

3. The `build` job checks that the tag, without the leading `v`, equals `<Version>` in `Directory.Build.props`. If they differ, the workflow fails before anything is published.
4. If the `nuget` environment has required reviewers, approve the `publish` job in the run.
5. The `publish` job logs in with Trusted Publishing, pushes the package with `--skip-duplicate`, and creates a GitHub Release with the `.nupkg` attached.

## Verify

- Package page: `https://www.nuget.org/packages/MetaphonePtBr/<version>`. Indexing can take several minutes after the push succeeds.
- GitHub Release: the release for the tag lists the `.nupkg` as an asset and has generated release notes.
- Optional: install the new version into a scratch project with `dotnet add package MetaphonePtBr --version <version>`.

## After the release

1. Set the API compatibility baseline to the version just released. In `src/MetaphonePtBr/MetaphonePtBr.csproj`, set:

   ```xml
   <PackageValidationBaselineVersion>3.0.0</PackageValidationBaselineVersion>
   ```

2. Bump `<Version>` in `Directory.Build.props` to the next version (for example `3.0.1` or `3.1.0`).
3. Keep an `[Unreleased]` section at the top of `CHANGELOG.md` for the changes that follow.

## Troubleshooting

| Symptom | Cause | Fix |
|---------|-------|-----|
| `NuGet/login` fails with 403 | The job is missing `id-token: write` | Keep the `permissions` block on the `publish` job as defined in `release.yml`. |
| `no matching policy` from NuGet/login | Workflow filename, environment or repository owner does not match the policy | Check the policy on nuget.org: `release.yml`, environment `nuget`, owner `crisbraatz`, repository `metaphone-pt-br`. |
| Version check step fails | The tag without `v` differs from `<Version>` in `Directory.Build.props` | Fix `<Version>` on `main` and push a new tag, or delete the wrong tag if it was never published. |
| Push reports the version already exists | That version was already published | Versions are permanent. Bump `<Version>` and release a new tag. `--skip-duplicate` keeps a re-run from failing on the same version. |
| Login succeeds but the push is unauthorized | The package is not owned by the policy account | Check the package owner on nuget.org. |
| Policy shows as "temporarily active" | Private repository, first publish still pending | Publish within the 7-day window; the policy becomes permanent after the first successful publish. |
| A re-run uses old workflow YAML | Re-running a run replays the original commit's workflow | Fix the obstacle and re-run; do not move the tag to a new commit. |
