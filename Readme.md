<h1 align="center"> 
    DESAIN ARSITEKTUR MICROSERVICES
</h1>

-------------------------

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
    },
  ]
}
```
