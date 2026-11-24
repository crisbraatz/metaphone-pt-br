# MetaphonePtBr — Visão de Negócio

## Visão
Biblioteca .NET de correspondência fonética para o português do Brasil, distribuída como pacote NuGet `MetaphonePtBr`. Converte uma palavra em um token fonético estável, de modo que grafias diferentes com o mesmo som (ex.: "Cristofer", "Christopher") gerem o mesmo token. Esta é uma reescrita completa (versão 3.0.0; a versão publicada atual é a 2.0.14) do projeto anterior (https://github.com/crisbraatz/MetaphonePtBrOld), guiada por IA e seguindo as boas práticas de pacotes NuGet.

## Problema e público
Desenvolvedores .NET que precisam buscar ou comparar nomes, endereços, marcas e produtos em português, tolerando erros de digitação e variações de grafia. Algoritmos fonéticos comuns (Soundex, Metaphone) são pensados para o inglês e falham no PT-BR.

## Proposta de valor
- Regras fonéticas específicas do PT-BR, documentadas e testadas.
- API simples, previsível e eficiente (baixa alocação de memória).
- Compatível com projetos antigos (`netstandard2.0`) e otimizada para projetos modernos (`net10.0`).
- Sem dependências externas.

## Modelo de negócio
Open source, gratuito, licença MIT.

## Usuários e papéis
- **Consumidores**: desenvolvedores .NET que instalam o pacote.
- **Mantenedor**: o autor do projeto, que publica as versões.
- **Contribuidores**: a comunidade, via issues e pull requests.

## Canais e plataformas
NuGet.org (pacote) e GitHub (código, issues, CI/CD com GitHub Actions).

## Dados e conformidade
A biblioteca não armazena nem transmite dados; processa apenas a palavra recebida, em memória. Não há impacto de LGPD/GDPR na biblioteca em si.

## Integrações
Nenhuma em tempo de execução. Na publicação: NuGet.org e GitHub Actions.

## Restrições
- Mesmo ID de pacote (`MetaphonePtBr`), nova versão principal (3.0.0) por SemVer.
- Breaking change assumida: API nova (ex.: `Encode` / `TryEncode`), sem métodos obsoletos de compatibilidade; guia de migração da 2.x.
- As regras fonéticas do README antigo são o ponto de partida e podem ser corrigidas quando houver casos errados, sempre documentadas.
- Alvos: `netstandard2.0` e `net10.0`.
- Contrato de entrada: uma única palavra, apenas letras do PT-BR (com acentos pré-compostos ou combinados).

## Métricas de sucesso (versão 3.0.0)
1. Todas as regras implementadas e cobertas por testes.
2. Publicação automática no NuGet via GitHub Actions.
3. Testes de mutação com Stryker.NET com alta pontuação de mutação.
4. Benchmarks mostrando desempenho igual ou melhor que a 2.x.
5. Documentação completa e guia de migração da 2.x para a 3.0.
6. Nova identidade visual: novo ícone e nova logo em `assets/brand/` (`icon.svg`, `icon.png`, `logo.svg`, `logo.png`, `metaphoneptbr.png`).

## Premissas e questões em aberto
- Créditos ao projeto original metaphone-ptbr de Carlos Costa Jordão são mantidos.
- (premissa) Meta de pontuação de mutação do Stryker.NET a definir no planejamento (proposta: ≥ 90%).
