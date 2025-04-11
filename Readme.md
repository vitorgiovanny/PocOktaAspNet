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


### 🐳 Docker Compose OpenLDAP


##  

Este `docker-compose.yml` inicia um container com OpenLDAP configurado com os seguintes parâmetros:

- Organização: **MinhaEmpresa**
- Domínio: **minhaempresa.local**
- Senha do admin: **admin**

```yaml
version: '3.8'

services:
  openldap:
    image: osixia/openldap:1.5.0
    container_name: openldap
    environment:
      - LDAP_ORGANISATION=MinhaEmpresa
      - LDAP_DOMAIN=minhaempresa.local
      - LDAP_ADMIN_PASSWORD=admin
      - KEEP_EXISTING_CONFIG="true"
    ports:
      - "389:389"
```


### fluxograma da nossa P.O.C
![Alt ou título da imagem](img/fluxograma.png)
