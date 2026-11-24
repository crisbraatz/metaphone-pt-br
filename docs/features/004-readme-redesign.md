# Funcionalidade 004 — README redesenhado

- Data: 2026-11-24 · Commit: (este commit) · Agentes: claude

## O que faz
Deixa a página do projeto no GitHub mais visual e fácil de ler, e cria uma versão própria e enxuta do README para a página do pacote no NuGet.

## O que mudou
- **README do GitHub:** logo centralizada, frase de destaque, linha de selos (NuGet, downloads, CI, frameworks, mutação ≥ 90%, licença), seções com emoji, diagrama do fluxo palavra → normalização → acentos → regras → token, e muito mais tabelas. A referência de regras ficou em blocos recolhíveis por letra, com as mesmas linhas de antes.
- **README do pacote NuGet** (`docs/nuget/README.md`): só Markdown, links absolutos e logo em PNG, porque o NuGet.org não mostra HTML. É ele que vai dentro do pacote.
- CHANGELOG atualizado.

## Decisões
- Dois READMEs: rico no GitHub, simples no NuGet.
- Os créditos ao projeto original (Carlos Costa Jordão) foram mantidos nos dois, como você pediu no início.

## Testes
Build, formatação e 2.154 testes passando. O README dentro do pacote foi comparado com o arquivo do repositório, e as tabelas de regras continuam idênticas.

## Notas de segurança
Só documentação e empacotamento mudaram; nenhum risco novo. A auditoria não encontrou problemas.

## Próximos passos
- Preparar o lançamento da 3.0.0 (configuração no nuget.org e tag `v3.0.0`).

1 aprendizado geral foi registrado para projetos futuros.
