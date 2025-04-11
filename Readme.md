### 🧠 Objetivo da POC

Utilizando aplicação ASP.NET, vamos realzizar uma autenticação B2C.

Testar a integração entre:
* OpenLDAP (container com base de usuários)
* Okta SSO (para autenticação centralizada) / OpenId Connect
* LDAP Agent da Okta (para sincronizar os usuários com o Okta)

### 🧰 Ferramentas
* Docker ou Docker Compose
* Imagem do OpenLDAP (v1.5.0)
* Conta na Okta Developer (https://developer.okta.com/)
* Okta LDAP Agent (precisamos baixar e instalar)
* [Opcional] Admin UI para o OpenLDAP, tipo o phpLDAPadmin

### Fluxograma
[Usuário acessa app]
        |
        v
[Redirecionado para SSO (AKTO)]
       |
       v
[Usuário digita login + senha no SSO]
       |
       v
[SSO envia dados de login ao Agent LDAP]
       |
       v
[Agent LDAP conecta no servidor LDAP]
       |
       v
[Agent tenta fazer BIND com DN + senha]
       |
       v
[LDAP responde]
  |         \
  |          \
[Sucesso]   [Falha]
   |            |
   v            v
[SSO cria sessão  ]     [SSO exibe erro de login]
[Usuário autenticado]
