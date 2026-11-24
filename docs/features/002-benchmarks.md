# Funcionalidade 002 — Benchmarks contra a 2.0.14

- Data: 2026-11-24 · Commit: (este commit) · Agentes: claude

## O que faz
Mede, lado a lado e com as mesmas palavras, a velocidade e a memória da versão 3.0.0 contra a 2.0.14 publicada. Resultado (Apple M4, .NET 10, máquina ociosa):

| Categoria | Tempo da 3.0.0 vs 2.0.14 | Memória da 3.0.0 vs 2.0.14 |
| :--- | :--- | :--- |
| Palavras curtas | 43% mais rápida | 69% menos |
| Médias | 23% mais rápida | 64% menos |
| Longas | 21% mais rápida | 60% menos |
| Acentuadas | 61% mais rápida | 54% menos |
| Amostra do gabarito | 39% mais rápida | 62% menos |

Detalhes em [docs/benchmarks.md](../benchmarks.md).

## O que mudou
- Projeto de benchmarks compara `Metaphone.Encode` (3.0.0) com `GetMetaphoneToken()` (2.0.14).
- Otimizações internas (sem mudar nenhum token nem a API): a normalização Unicode só roda quando necessária, a simplificação de acentos só copia a palavra quando há acento, e o token é montado num buffer fixo.
- Novo workflow do GitHub Actions "Benchmarks", disparado manualmente, que publica o resultado no resumo da execução e como artefato.

## Decisões
- As duas versões têm o mesmo nome de assembly; a 2.0.14 é carregada isoladamente num contexto próprio, a partir do pacote oficial, com verificação de integridade (SHA-256) antes de rodar (ADR-003).
- Os benchmarks não rodam em cada PR (lentos e instáveis na nuvem); os números locais em máquina ociosa são a referência.

## Testes
2.154 testes passando (inclui as 1.799 palavras idênticas à 2.0.14 e um novo teste que garante que nenhuma regra gera tokens grandes demais para o buffer). Pontuação de mutação: 97,33%.

## Notas de segurança
A biblioteca publicada não mudou em termos de risco. A versão antiga só é usada nos benchmarks (não vai no pacote), vem do NuGet oficial e é conferida byte a byte antes de ser carregada. O novo workflow só lê o repositório e não usa segredos.

## Próximos passos
- Rodar o workflow "Benchmarks" uma vez no GitHub (opção `short`) para confirmar que funciona lá.
- Documentação da 3.0.0 (README com a nova API, tabela de regras, guia de migração).

3 aprendizados gerais foram registrados para projetos futuros.
