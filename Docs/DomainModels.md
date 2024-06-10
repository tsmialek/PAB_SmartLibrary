# Domain Models

## User

```json
{
  "id": (new guid),
  "firstName": "Tomek",
  "lastName": "Smialek",
  "email": "tomek@wsei.pl",
  "password": "wsei@123",
  "Roles": [
    "roleName",
  ]
}
```

## Book

```json
{
  "id": (new guid),
  "Title": "example",
  "Author": "example",
  "ISBN": "1111-1111-1111-1111",
  "Description": "example",
  "PageCount": 100,
  "Date": 01/01/2020
}
```

## Role

```json
{
  "id": (new guid),
  "Name": "example",
  "Users": [
    {user object},
  ]
}
```
