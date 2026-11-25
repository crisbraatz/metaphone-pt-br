# Funcionalidade 005 — Preparar o lançamento da 3.0.0

- Data: 2026-11-24 · Commit: (este commit) · Agentes: claude

## O que faz
Deixa o projeto pronto para publicar a 3.0.0 com segurança. Falta só você fazer a configuração externa (nuget.org e GitHub) e criar a tag.

## O que mudou
- **READMEs:** o aviso de "ainda não lançada" saiu; a 3.0.0 é apresentada como a versão atual.
- **CHANGELOG:** seção `[3.0.0] - 2026-11-24` organizada (adicionado, alterado, removido) e links de comparação corretos.
- **Workflow de release:**
  - modo **ensaio** (botão manual em Actions → Release) que compila, testa e gera o pacote sem publicar;
  - publicação real só com tag `v*`, e só se a tag for igual à versão do projeto;
  - o pacote é gerado uma vez sem permissões especiais e só depois publicado (Trusted Publishing, sem chave guardada no repositório).
- **Guia de lançamento** em `docs/releasing.md`, com o passo a passo da configuração e da publicação.

## Decisões
- Criar a tag e publicar fica com você, depois da configuração no nuget.org e no GitHub.

## Testes
Build, formatação e 2.154 testes passando; mutação em 97,33%. Pacote final conferido: metadados, ícone, README, `netstandard2.0` e `net10.0`, símbolos de depuração embutidos com SourceLink, sem dependências.

## Notas de segurança
A auditoria não encontrou problemas graves. O modo ensaio não consegue publicar, a publicação usa credenciais temporárias (OIDC) e todas as actions estão fixadas por versão exata. Uma sugestão leve foi aplicada: a chave temporária do NuGet passa por variável de ambiente.

## Próximos passos
Seguir o guia: configurar o nuget.org e o GitHub, rodar o ensaio e criar a tag `v3.0.0`.

1 aprendizado geral foi registrado para projetos futuros.
