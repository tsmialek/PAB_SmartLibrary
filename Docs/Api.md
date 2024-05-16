### Login Response

<!-- prettier-ignore -->
- [SmartLibraryApi](#smart-library-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)

## Auth

### Register

```js
curl --location 'https://localhost:7207/auth/register' \
--header 'Content-Type: application/json' \
--data-raw '{
    "firstName": "Tomek",
    "lastName": "Smialek",
    "email": "tomek@wsei.pl",
    "password": "wsei@123"
}'
```

#### Register Request

```json
{
  "firstName": "Tomek",
  "lastName": "Smialek",
  "email": "tomek@wsei.pl",
  "password": "wsei@123"
}
```

#### Register Response

```js
200 OK
```

```json
{
  "id": 00000-00000-0000-000,
  "firstName": "Tomek",
  "lastName": "Smialek",
  "email": "tomek@wsei.pl",
  "token": "eyASk...aSDFhbnQ"
}
```

### Login

```js
curl --location 'https://localhost:7207/auth/login' \
--header 'Content-Type: application/json' \
--data-raw '{
    "email": "tomek@wsei.pl",
    "password": "wsei@123"
}'
```

#### Login Request

```json
{
  "email": "tomek@wsei.pl",
  "password": "wsei@123"
}
```

#### Login Response

```js
200 OK
```

```json
{
  "id": 00000-00000-0000-000,
  "firstName": "Tomek",
  "lastName": "Smialek",
  "email": "tomek@wsei.pl",
  "token": "eyASk...aSDFhbnQ"
}
```
