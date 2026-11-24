# Security Policy

## Supported versions

| Version | Supported |
| :--- | :--- |
| 3.x | ✅ |
| 2.x | ❌ (upgrade to 3.x) |

## Reporting a vulnerability
Please **do not** open a public issue. Report it privately through
[GitHub Security Advisories](https://github.com/crisbraatz/MetaphonePtBr/security/advisories/new).

You can expect an acknowledgement within 7 days. Once confirmed, a fix is released as a patch
version and the advisory is published with credit to the reporter (unless you prefer otherwise).

## Scope
MetaphonePtBr performs no I/O, networking or persistence; it only processes the string it receives.
Relevant reports include crashes, unbounded resource usage or incorrect handling of untrusted input.
