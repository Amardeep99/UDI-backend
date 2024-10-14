# API Endpoint Documentation

This document describes the endpoints for the UDI (Norwegian Directorate of Immigration) backend API.

## Base URL

All endpoints are prefixed with `/api/v1`.

## Endpoints

### 1. Check if Reference Exists

- **URL:** `/referanse/{id}`
- **Method:** GET
- **Description:** Checks if a reference with the given ID exists.
- **Parameters:**
  - `id` (path parameter): The ID of the reference to check.
- **Responses:**
  - 200 OK: Returns a boolean indicating whether the reference exists.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```
GET /api/v1/referanse/12345
```

**Example Response:**
```json
true
```

### 2. Create Application

- **URL:** `/soknad`
- **Method:** POST
- **Description:** Creates a new application.
- **Request Body:**
  ```json
  {
    "DNumber": "string",
    "TravelDate": "string (ISO 8601 date format)"
  }
  ```
- **Responses:**
  - 200 OK: Returns the ID of the created application.
  - 400 Bad Request: If the request body is null or invalid.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```http
POST /api/v1/soknad
Content-Type: application/json

{
  "DNumber": "12345678901",
  "TravelDate": "2024-05-15"
}
```

**Example Response:**
```json
67890
```

### 3. Create Reference

- **URL:** `/referanse/{aID}`
- **Method:** POST
- **Description:** Creates a new reference for an application.
- **Parameters:**
  - `aID` (path parameter): The ID of the application to create a reference for.
- **Responses:**
  - 200 OK: Returns the ID of the created reference.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```
POST /api/v1/referanse/67890
```

**Example Response:**
```json
54321
```

### 4. Create Form

- **URL:** `/skjema`
- **Method:** POST
- **Description:** Creates a new form.
- **Request Body:**
  ```json
  {
    "OrganisationNr": "string",
    "ReferenceId": "integer",
    "HasObjection": "boolean",
    "ObjectionReason": "string",
    "HasDebt": "boolean",
    "OrganisationName": "string",
    "Email": "string",
    "Phone": "string",
    "ContactName": "string"
  }
  ```
- **Responses:**
  - 200 OK: Returns the ID of the created form.
  - 400 Bad Request: If the request body is null or invalid.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```http
POST /api/v1/skjema
Content-Type: application/json

{
  "OrganisationNr": "123456789",
  "ReferenceId": 54321,
  "HasObjection": false,
  "ObjectionReason": "",
  "HasDebt": false,
  "OrganisationName": "Example Company AS",
  "Email": "contact@example.com",
  "Phone": "+4712345678",
  "ContactName": "John Doe"
}
```

**Example Response:**
```json
98765
```

## Error Handling

All endpoints return a 500 Internal Server Error status code if an unexpected exception occurs during processing. The specific error message is not returned to the client for security reasons.

**Example Error Response:**
```http
HTTP/1.1 500 Internal Server Error
Content-Type: application/json

{
  "error": "An unexpected error occurred"
}
```

## Notes
