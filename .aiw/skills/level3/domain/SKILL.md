---
name: domain
description: Load when working on MetaphonePtBr phonetic rules, input contract, tokens, public API or release/versioning decisions.
level: 3
updated: 2026-11-24
---

# Domínio — MetaphonePtBr

## Glossário
- **Palavra**: entrada única, apenas letras do português do Brasil.
- **Token fonético**: código em letras maiúsculas que representa o som da palavra (ex.: `KRSTF`). Não é a palavra original.
- **Regra fonética**: transformação aplicada a uma letra conforme seu contexto (letras anteriores e seguintes, início ou fim da palavra).
- **Letra mutável**: letra cujo som depende do contexto (C, G, H, L, N, P, Q, R, S, T, W, X, Y, Z, vogais).
- **Letra imutável**: sempre o mesmo som (B, D, F, J, K, M, V).
- **Bypass (`0`)**: a letra não emite som no token.
- **Normalização**: validar a entrada e convertê-la para a forma canônica (maiúsculas, acentos pré-compostos).

## Regras de negócio
1. A entrada deve ser uma única palavra com apenas letras; caso contrário é rejeitada. (fonte: entrevista / README legado)
2. Letras acentuadas aceitas: `ÁÀÂÃÉÊÍÓÔÕÚÜÇ` e minúsculas, pré-compostas ou com marcas combinantes. (fonte: entrevista)
3. São rejeitados: dígitos, espaços, símbolos, hífens, apóstrofos, ligaduras, indicadores ordinais e letras fora do PT-BR. (fonte: entrevista)
4. Maiúsculas e minúsculas geram o mesmo token. (fonte: entrevista)
5. A palavra é lida da esquerda para a direita; cada regra pode olhar a letra atual, as duas anteriores e as duas seguintes. (fonte: README legado)
6. As regras são avaliadas por prioridade; se nenhuma regra especial casar, a letra mutável usa seu som padrão. (fonte: README legado)
7. A tabela de regras do README legado é o ponto de partida; correções são permitidas quando um caso estiver errado, sempre documentadas no README e no CHANGELOG. (fonte: entrevista)
8. Grafias como Christofer, Christofr, Christopher, Christophr, Cristofer, Cristofr e Cristophr devem gerar `KRSTF`. (fonte: README legado)

9. Antes das regras, os acentos são simplificados para A–Z mais Ç (Ü vira U, Á vira A etc.); Ç é tratado pela regra do C (som S). (fonte: feature 001)
10. Palavras formadas só por letras mudas (ex.: "H", "RR", "LH") geram token vazio. (fonte: feature 001)
11. Até decisão contrária, os tokens devem ser idênticos aos da versão 2.0.14 (gabarito com ~1800 palavras nos testes). (fonte: feature 001)

## Invariantes
- Mesma entrada válida → sempre o mesmo token (determinístico, em qualquer framework alvo).
- O token contém apenas letras maiúsculas A–Z (pode ser vazio).
- Entrada inválida nunca gera token: `Encode` lança exceção, `TryEncode` retorna falso.

## Fluxos
### Lançamento de versão
1. Atualizar CHANGELOG (seção da versão).
2. Criar a tag `vX.Y.Z` (SemVer: quebra de API → versão principal).
3. A CI publica no NuGet e cria a GitHub Release.

### Versão 3.0.0
- Mesmo pacote `MetaphonePtBr`, quebra de compatibilidade intencional, sem métodos obsoletos; guia de migração da 2.x obrigatório.
