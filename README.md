# BrisMac PDV — Sistema de Ponto de Venda e Emissão de NFC-e

Sistema desktop de Ponto de Venda (PDV) desenvolvido em **C# (Windows Forms)** e **SQL Server**, projetado para atender operações de varejo com frente de caixa ágil, controle de estoque integrado e emissão opcional de **NFC-e (Nota Fiscal de Consumidor Eletrônica)** via **ACBr API**.

## Funcionalidades

- **Frente de Caixa (PDV):**
  - Lançamento rápido de produtos por leitor de código de barras ou pesquisa por nome/código interno.
  - Alteração de quantidades, remoção de itens e cálculo automático de subtotais e total geral.
  - Fechamento de venda (atalho `F12`) com cálculo de troco e suporte a **Dinheiro, Cartão de Crédito, Cartão de Débito e PIX**.
- **Emissão Fiscal (NFC-e):**
  - Emissão opcional por venda (permite registrar vendas comuns sem cupom fiscal ou emitir NFC-e sob demanda).
  - Suporte aos ambientes **Sandbox**, **Homologação** e **Produção**.
  - Validação de CPF/CNPJ do consumidor na nota, controle automático de série e numeração, e geração/visualização do DANFE NFC-e em PDF.
  - **Resiliência offline:** caso ocorra queda de internet ou rejeição da SEFAZ, a venda permanece salva no banco de dados local sem travar o atendimento no balcão.
- **Cadastro e Controle de Estoque:**
  - Cadastro rápido de produtos com descrição, preço de venda, controle de quantidade, localização na prateleira, aplicação por modelo de moto e suporte a código de barras (GTIN) ou itens sem código (`SEM GTIN`).
  - Preenchimento automático dos parâmetros fiscais padrão na emissão da NFC-e, simplificando o cadastro no dia a dia da loja.
  - Baixa automática de estoque a cada venda concluída.
- **Gestão Administrativa:**
  - Cadastros de clientes e fornecedores.
  - Controle de abertura/fechamento de caixa e histórico detalhado de vendas com status fiscal.
  - Tela de configuração fiscal da empresa (dados cadastrais, regime tributário CRT, código IBGE e credenciais da API armazenadas diretamente no banco de dados).

## Tecnologias Utilizadas

- **Linguagem & Interface:** C# (.NET Framework / Windows Forms)
- **Banco de Dados:** Microsoft SQL Server Express (`System.Data.SqlClient`)
- **Integração Fiscal:** ACBr API (REST / JSON) para autorização de NFC-e e geração de DANFE

## Segurança e Arquitetura

- Nenhuma credencial sensível, certificado digital (`.pfx`) ou chave de API é armazenada no código-fonte.
- Os parâmetros fiscais e as credenciais de produção (`Client ID` e `Client Secret`) são persistidos exclusivamente na tabela de configuração (`tbempresa`) do banco de dados local da loja.

## Telas do Sistema

### Menu Principal
<img width="1915" height="1026" alt="Menu Principal" src="https://github.com/user-attachments/assets/aa5f789a-6d3a-42eb-8532-932b1220ec8b" />

### Frente de Caixa (PDV)
<img width="1909" height="1023" alt="Frente de Caixa" src="https://github.com/user-attachments/assets/b3537c7b-11fb-4fe5-9427-c4e3673bd3f2" />

### Fechamento de Venda (F12) e NFC-e
<img width="797" height="482" alt="Fechamento de Venda" src="https://github.com/user-attachments/assets/0847ec1c-c763-4f6e-98dd-adcadb8dc78b" />

### Consulta de Produtos e Estoque
<img width="1010" height="742" alt="Consulta de Produtos" src="https://github.com/user-attachments/assets/1bb541eb-3abb-47b4-bc3c-bbcb96ccbe4d" />

## Autor

Desenvolvido por **Matheus Almeida Nalesso** — [@Mthsdrag](https://github.com/Mthsdrag)
