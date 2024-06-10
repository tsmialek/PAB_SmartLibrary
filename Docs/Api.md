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
    - [List users](#list-users)
    - [Get user by id](#get-user-by-id)
    - [Get user by email](#get-user-by-email)
  - [Books](#books)
    - [List books](#list-books)
    - [Add book](#add-book)
    - [Delete book](#delete-book)
    - [Get book by id](#get-book-by-id)
    - [Get book by email](#get-book-by-email)
  - [Graph QL](#graph-ql)
    - [Books](#books)
      - [All Books](#all-books)
      - [Book by id](#book-by-id)
  - [Roles](#roles)
    - [List roles](#list-roles)
    - [Add role to user](#add-role-to-user)

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

---

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

---

### List users

```js
curl --location 'https://localhost:7207/auth/all' \
--header 'Authorization: {{token}} \
--data ''
```

#### List users request

```js
'https://localhost:7207/auth/all';
```

#### List users response

```js
[
  {
    id: '1c8e7677-efdc-4ddf-b8af-1c1c52f67015',
    firstName: 'Kamil',
    lastName: 'Ślimak',
    email: 'kamil@wsei.pl',
    password: 'wsei@123',
    roles: [
      {
        id: '1a6590fc-0644-40f2-9ea4-939fafc3e3eb',
        name: 'Admin',
      },
      {
        id: '1ead1b7c-f356-4911-8f78-9ccaab78f0b0',
        name: 'User',
      },
    ],
  },
];
```

---

### Get user by id

```js
curl --location 'https://localhost:7207/auth/id/4b06c5f7-e899-425d-878c-9173ad44d65e' \
--header 'Authorization: {{token}} \
--data ''
```

#### Get user by Id request

```js
'https://localhost:7207/auth/id/4b06c5f7-e899-425d-878c-9173ad44d65e';
```

#### Get user by Id response

```js
{
    "id": "4b06c5f7-e899-425d-878c-9173ad44d65e",
    "firstName": "Tomasz",
    "lastName": "Smialek",
    "email": "admin@wsei.pl",
    "password": "admin",
    "roles": [
        {
            "id": "1a6590fc-0644-40f2-9ea4-939fafc3e3eb",
            "name": "Admin"
        }
    ]
}
```

---

### Get user by email

```js
curl --location 'https://localhost:7207/auth/email/admin@wsei.pl' \
--header 'Authorization: {{token}} \
--data ''
```

#### Get user by Id request

```js
'https://localhost:7207/auth/email/admin@wsei.pl';
```

#### Get user by Id response

```js
{
    "id": "4b06c5f7-e899-425d-878c-9173ad44d65e",
    "firstName": "Tomasz",
    "lastName": "Smialek",
    "email": "admin@wsei.pl",
    "password": "admin",
    "roles": [
        {
            "id": "1a6590fc-0644-40f2-9ea4-939fafc3e3eb",
            "name": "Admin"
        }
    ]
}
```

---

### Books

#### List books

```js
curl --location 'https://localhost:7207/books' \
--header 'Authorization: {{token}} \
--data ''
```

#### List books request

```js
'https://localhost:7207/books';
```

#### List books response

```js
[
  {
    id: 'a46e2115-fc6a-4454-a778-0af6075cfdb5',
    title: 'Wiedźmin - Sezon burz',
    author: 'Andrzej Sapkowski',
    isbn: '123-123-123',
    description: 'Wiedźmin cz. 1',
    pageCount: 420,
    date: '2023-05-19',
  },
];
```

---

#### Add book

```js
curl --location 'https://localhost:7207/books' \
--header 'Content-Type: application/json' \
--header 'Authorization: ••••••' \
--data '{
    "title": "Wiedźmin - Pani Jeziora",
    "author": "Andrzej Sapkowski",
    "isbn": "123-123-123",
    "description": "Wiedźmin cz. 1",
    "pageCount": 420,
    "date": "2023-05-19"
}'
```

#### Add book request

```js
{
    "title": "Wiedźmin - Pani Jeziora",
    "author": "Andrzej Sapkowski",
    "isbn": "123-123-123",
    "description": "Wiedźmin cz. 1",
    "pageCount": 420,
    "date": "2023-05-19"
};
```

#### Add book response

```js
{
    "id": "c5cfecbb-bbb7-4d62-8ac4-15b99e4a5b2c",
    "title": "Wiedźmin - Pani Jeziora",
    "author": "Andrzej Sapkowski",
    "isbn": "123-123-123",
    "description": "Wiedźmin cz. 1",
    "pageCount": 420,
    "date": "2023-05-19"
}
```

---

#### Delete book

```js
curl --location --request DELETE 'https://localhost:7207/books/{{id}}' \
--header 'Authorization: {{token}}
```

#### Delete book request

```js
'https://localhost:7207/books/{{id}}';
```

#### delete book response

```js
{
    "id": "c5cfecbb-bbb7-4d62-8ac4-15b99e4a5b2c",
    "title": "Wiedźmin - Pani Jeziora",
    "author": "Andrzej Sapkowski",
    "isbn": "123-123-123",
    "description": "Wiedźmin cz. 1",
    "pageCount": 420,
    "date": "2023-05-19"
}
```

---

#### Get book by id

```js
curl --location 'https://localhost:7207/books/{{id}}' \
--header 'Authorization: {{token}} \
--data ''
```

#### Get book by id request

```js
'https://localhost:7207/books/{{id}}';
```

#### Get book by id response

```js
{
    "id": "3604a4d2-4755-4331-9404-2ca5d568b269",
    "title": "Example Book Title",
    "author": "John Doe",
    "isbn": "123-456-789",
    "description": "This is an example book description.",
    "pageCount": 300,
    "date": "2023-05-19"
}
```

---

#### Get book by name

```js
curl --location 'https://localhost:7207/books/search?name={{name}}' \
--header 'Authorization: ••••••'
```

#### Get book by name request

```js
'https://localhost:7207/books/search?name={{name}}';
```

#### Get book by name response

```js
{
    "id": "3604a4d2-4755-4331-9404-2ca5d568b269",
    "title": "Example Book Title",
    "author": "John Doe",
    "isbn": "123-456-789",
    "description": "This is an example book description.",
    "pageCount": 300,
    "date": "2023-05-19"
}
```

---

### Graph QL

#### Books

#### All books

```js
curl --location 'https://127.0.0.1:7207/graphql' \
--header 'Content-Type: application/json' \
--data '{"query":"{\r\n  books {\r\n    id\r\n    title\r\n    author\r\n    iSBN\r\n  }\r\n}","variables":{}}'
```

#### All books request

```js
{
  books {
    id
    title
    author
    iSBN
  }
};
```

#### All books response

```js
{
    "data": {
        "books": [
            {
                "id": "a46e2115-fc6a-4454-a778-0af6075cfdb5",
                "title": "Wiedźmin - Sezon burz",
                "author": "Andrzej Sapkowski",
                "iSBN": "123-123-123"
            }
        ]
    }
}
```

---

#### Book by id

```js
curl --location 'https://localhost:7207/graphql' \
--header 'Content-Type: application/json' \
--data '{"query":"{\r\n  book(id: \"{{id}}\") {\r\n    id\r\n    title\r\n    author\r\n    iSBN\r\n  }\r\n}","variables":{}}'
```

#### Book by id request

```js
{
  book(id: "{{id}}") {
    id
    title
    author
    iSBN
  }
}
```

#### Book by id response

```js
{
    "data": {
        "books": [
            {
                "id": "a46e2115-fc6a-4454-a778-0af6075cfdb5",
                "title": "Wiedźmin - Sezon burz",
                "author": "Andrzej Sapkowski",
                "iSBN": "123-123-123"
            }
        ]
    }
}
```

---

### Roles

#### List roles

```js
curl --location 'https://localhost:7207/roles' \
--header 'Authorization: {{token}} \
--data ''
```

#### List roles request

```js
'https://localhost:7207/roles';
```

#### List roles response

```js
[
  {
    id: '1ead1b7c-f356-4911-8f78-9ccaab78f0b0',
    name: 'User',
    users: [
      {
        id: '1c8e7677-efdc-4ddf-b8af-1c1c52f67015',
        firstName: 'Kamil',
        lastName: 'Ślimak',
        email: 'kamil@wsei.pl',
        password: 'wsei@123',
      },
      {
        id: 'e1616682-d464-4f62-9eca-620658170210',
        firstName: 'Piotr',
        lastName: 'Skowyrski',
        email: 'piotr@wsei.pl',
        password: 'wsei@123',
      },
    ],
  },
];
```

---

#### Add role to user

```js
curl --location 'https://localhost:7207/roles/addUserRoleByEmail' \
--header 'Content-Type: application/json' \
--header 'Authorization: {{token}} \
--data-raw '{
    "userEmail": "swica@wsei.pl",
    "newUserRoleName": "Admin"
}'
```

#### Add role to user

```js
{
    "userEmail": "swica@wsei.pl",
    "newUserRoleName": "Admin"
}
```

#### Add role to user

```js
{
    "id": "ba891d0c-9ff5-49aa-8f41-b7f05bd00ba3",
    "firstName": "Kamil",
    "lastName": "Świca",
    "email": "swica@wsei.pl",
    "password": "wsei@123",
    "roles": [
        {
            "id": "1ead1b7c-f356-4911-8f78-9ccaab78f0b0",
            "name": "User"
        },
        {
            "id": "1a6590fc-0644-40f2-9ea4-939fafc3e3eb",
            "name": "Admin"
        }
    ]
}
```
