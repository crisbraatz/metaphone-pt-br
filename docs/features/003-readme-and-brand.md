# Funcionalidade 003 — README completo e nova identidade visual

- Data: 2026-11-24 · Commit: (este commit) · Agentes: claude

## O que faz
Dá ao projeto uma identidade visual nova e um README completo para a 3.0.0, que aparece tanto no GitHub quanto na página do pacote no NuGet.

## O que mudou
- **Nova identidade (conceito A, escolhido por você):** ícone com um losango e um círculo (geometria da bandeira, sem a paleta óbvia) e barras de som que terminam num ponto dourado — o token. Logo só com ícone e nome, sem slogan, nome em Space Grotesk Bold (fonte livre, licença SIL OFL) convertido em vetores. Paleta: azul-marinho, esmeralda e dourado. Arquivos em `assets/brand/`.
- **README completo:** instalação, início rápido com `Encode`/`TryEncode`, exemplo de correspondência, casos de uso, contrato de entrada, erros, como funciona, tabela de regras completa, desempenho, migração da 2.x, frameworks suportados, desenvolvimento, publicação, contribuição, créditos e licença.
- CHANGELOG atualizado.

## Decisões
- O guia de migração ficou como seção do README (mais fácil de achar) em vez de arquivo separado.
- Imagens e links do README usam endereços absolutos para funcionarem também na página do NuGet.

## Testes
Build, formatação e 2.154 testes passando. O README e o ícone dentro do pacote NuGet foram comparados byte a byte com os do repositório. As tabelas de regras são idênticas às da versão anterior.

## Notas de segurança
Só documentação e imagens mudaram; nenhum risco novo. A auditoria não encontrou problemas.

## Próximos passos
- As imagens do README só aparecem depois do push (elas apontam para a `main` no GitHub).
- Correções de regras fonéticas, se desejado.

1 aprendizado geral foi registrado para projetos futuros.
