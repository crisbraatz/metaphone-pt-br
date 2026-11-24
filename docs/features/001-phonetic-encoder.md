# Funcionalidade 001 — API pública e motor de regras fonéticas

- Data: 2026-11-24 · Commit: (este commit) · Agentes: claude

## O que faz
A biblioteca agora gera tokens fonéticos de verdade. `Metaphone.Encode("Cristopher")` retorna `KRSTF`, e `Metaphone.TryEncode` faz o mesmo sem lançar exceção quando a entrada é inválida. Todas as regras do README antigo (C, G, H, imutáveis, L, N, P, Q, R, S, T, vogais, W, X, Y, Z) foram reescritas.

## O que mudou
- Nova API pública: só a classe `Metaphone` (`Encode` e `TryEncode`). O antigo `GetMetaphoneToken()` deixou de existir (quebra intencional da 3.0.0, registrada no CHANGELOG).
- Motor de regras interno: uma classe por grupo de letras, lendo a palavra da esquerda para a direita.
- A entrada é validada e os acentos são simplificados antes das regras (Ü vira U; Ç continua Ç), como na 2.0.14.

## Decisões
- **A 2.0.14 é o gabarito:** um arquivo com 1.799 palavras e seus tokens, gerado pelo pacote 2.0.14 publicado, faz parte dos testes. Qualquer diferença quebra o build.
- Palavras que só têm letras mudas (ex.: "H", "RR") retornam token vazio, igual à 2.0.14.

## Testes
2.153 testes passando (regras linha a linha, API, e as 1.799 palavras idênticas à 2.0.14). Pontuação de mutação: 100%. Build sem avisos em `netstandard2.0` e `net10.0`.

## Notas de segurança
Nada mudou em termos de risco: a biblioteca não acessa rede, disco nem segredos, e toda entrada é verificada contra a lista de letras permitidas antes de qualquer regra rodar. A auditoria não encontrou problemas.

## Próximos passos
- Benchmarks comparando com a 2.0.14.
- Guia de migração da 2.x e README com a nova API e a tabela de regras.
- Correções de regras (agora possíveis com segurança, pois o gabarito mostra exatamente o que muda).

2 aprendizados gerais foram registrados para projetos futuros.
