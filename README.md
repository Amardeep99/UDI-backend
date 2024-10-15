# API Endpoint Documentation

This document describes the endpoints for the UDI (Norwegian Directorate of Immigration) backend API.

## Base URL

All endpoints are prefixed with `/api/v1`.

## Endpoints

### 1. Get Reference

- **URL:** `/referanse/{id}`
- **Method:** GET
- **Description:** Retrieves information about a reference with the given ID.
- **Parameters:**
  - `id` (path parameter): The ID of the reference to retrieve.
- **Responses:**
  - 200 OK: Returns a JSON object with reference information.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```
GET /api/v1/referanse/12345
```

**Example Response:**
```json
{
  "ReferenceExists": true,
  "FormID": 67890
}
```

### 2. Get Form

- **URL:** `/skjema/{formId}`
- **Method:** GET
- **Description:** Retrieves a form with the given ID.
- **Parameters:**
  - `formId` (path parameter): The ID of the form to retrieve.
- **Responses:**
  - 200 OK: Returns the Form object.
  - 404 Not Found: If the form with the given ID doesn't exist.

**Example Request:**
```
GET /api/v1/skjema/67890
```

**Example Response:**
```json
{
  "id": 67890,
  "organisationNr": "123456789",
  "referenceId": 54321,
  "hasObjection": false,
  "objectionReason": "",
  "hasDebt": false,
  "organisationName": "Example Company AS",
  "email": "contact@example.com",
  "phone": "+4712345678",
  "contactName": "John Doe"
}
```

### 3. Create Application

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

### 4. Create Reference

- **URL:** `/referanse/{aID}`
- **Method:** POST
- **Description:** Creates a new reference for an application.
- **Parameters:**
  - `aID` (path parameter): The ID of the application to create a reference for.
- **Responses:**
  - 200 OK: Returns the ID of the created reference.
  - 404 Not Found: If the application with the given ID doesn't exist.
  - 500 Internal Server Error: If an unexpected error occurs.

**Example Request:**
```
POST /api/v1/referanse/67890
```

**Example Response:**
```json
54321
```

### 5. Create Form

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
  - 400 Bad Request: If the request body is null, invalid, or if the reference already has a form ID.
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

- All endpoints return a 500 Internal Server Error status code if an unexpected exception occurs during processing.
- Specific error messages are returned for 400 Bad Request and 404 Not Found responses.
- The CreateApplication endpoint may return a 400 Bad Request with a specific error message for invalid data.
- The CreateForm endpoint may return a 400 Bad Request if the reference already has a form ID.

**Example Error Response:**
```http
HTTP/1.1 400 Bad Request
Content-Type: application/json

{
  "error": "Reference already has a form ID"
}
```