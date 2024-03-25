<h1 align="center"> 
    DESAIN ARSITEKTUR MICROSERVICES
</h1>

---

### Overview

This Example Minimal API uses sql (SqlServer) With the help of WEB ASP NET CORE, this REST API is an experiment in building a simple REST API. Later this API will be FETCHed with React JS

### Setting Up Project

<b>Clone the project into your local directory</b>

```
git clone https://github.com/WayanBerdyanto/72210481_UTS.git
```

<b>Install SQL SERVER</b>
you can install visit in [SqlServer Download](https://www.mongodb.com/docs/mongodb-shell/run-commands/)

<b>See Documentation</b>

```
dotnet watch run
```

# Endpoint Documentation

## Endpoint GetCategory

- **URL:** `http://localhost:5222/api/getAllCategory`
- **Method:** GET

## Example Response

```json
{
  "success": true,
  "message": "request update successful",
  "data": [
    {
      "categoryID": 1,
      "categoryName": "Food"
    },
    {
      "categoryID": 2,
      "categoryName": "Drink"
    }
  ]
}
```

## Endpoint GetCategoryById

- **URL:** `http://localhost:5222/api/getCategoryById/{id}`
- **Method:** GET

## Example Response

```json
{
  "success": true,
  "message": "request data successful",
  "data": [
    {
      "categoryID": 1,
      "categoryName": "Food"
    }
  ]
}
```

## Endpoint SearchNameCategory

- **URL:** `http://localhost:5222/api/getCategory/search/{categoryName}`
- **Method:** GET

## Example Response

```json
{
  "success": true,
  "message": "request successful",
  "data": [
    {
      "categoryID": 1,
      "categoryName": "Food"
    }
  ]
}
```
## Endpoint InsertCategory

- **URL:** `http://localhost:5222/api/category`
- **Method:** POST

## Example Input
```json
{
  "categoryName": "Pizza"
}
```
## Example Response
```json
{
  "categoryID": 0,
  "categoryName": "Pizza"
}
```
## Endpoint UpdateCategory

- **URL:** `http://localhost:5222/api/category`
- **Method:** PUT

## Example Input
```json
{
  "categoryID": 3,
  "categoryName": "Burger"
}
```
## Example Response
```json
{
  "success": true,
  "message": "request update successful",
  "data": {
    "categoryID": 3,
    "categoryName": "Burger"
  }
}
```
## Endpoint DeleteCategory

- **URL:** `http://localhost:5222/api/category`
- **Method:** DELETE

## Example Input
```json
{
  "categoryID": 3,
}
```
## Example Response
```json
{
  "success": true,
  "message": "request delete successful"
}
```
