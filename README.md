# UDI Backend API Documentation

Welcome to the documentation for the UDI (Norwegian Directorate of Immigration) Backend API. This API allows you to interact with the UDI application system, managing applications, references, and forms related to immigration processes.

## Table of Contents

1. [Introduction](#introduction)
2. [Authentication](#authentication)
3. [API Endpoints](#api-endpoints)
4. [Request/Response Formats](#request-response-formats)
5. [Error Handling](#error-handling)
6. [Rate Limiting](#rate-limiting)
7. [Examples](#examples)

## Introduction

The UDI Backend API is a RESTful service that provides endpoints for creating and managing immigration applications, references, and forms. It uses JSON for request and response bodies.

Base URL: `https://udi.azurewebsites.net/api/v1/`

## Authentication
Authentication is not handled in the backend of the solution. 

## API Endpoints

The service exposes the following API endpoints:

- `GET /api/v1/referanse/{refid}`: Get reference details
- `GET /api/v1/skjema/{formId}`: Get form details
- `POST /api/v1/soknad`: Create a new application
- `POST /api/v1/referanse`: Create a new reference
- `POST /api/v1/skjema`: Create a new form
- `PUT /api/v1/skjema/{id}`: Edit an existing form

### Get Reference Details

- **URL**: `/referanse/{refid}`
- **Method**: GET
- **URL Params**: 
  - `refid` (integer, required): The reference ID
- **Success Response**: 
  - Code: 200
  - Content: JSON object containing reference details including:
    - ReferenceExists (boolean)
    - FormId (integer, nullable)
    - TravelDate (string, nullable, format: "YYYY-MM-DD")
    - OrganisationNr (integer)
    - ApplicantName (string)
    - OrganisationName (string)
    - Deadline (datetime, nullable)

### Get Form Details

- **URL**: `/skjema/{formId}`
- **Method**: GET
- **URL Params**:
  - `formId` (integer, required): The form ID
- **Success Response**:
  - Code: 200
  - Content: Form object containing all form details
- **Error Response**:
  - Code: 404
  - Content: Error message when form is not found

### Create Application

- **URL**: `/soknad`
- **Method**: POST
- **Data Params**: JSON object (see [CreateApplicationRequest](#createapplicationrequest))
- **Success Response**:
  - Code: 200
  - Content: Integer (application ID)
- **Error Response**:
  - Code: 400
  - Content: Error message if data is invalid or if person already has an ongoing process

### Create Reference

- **URL**: `/referanse`
- **Method**: POST
- **Data Params**: JSON object (see [CreateReferenceRequest](#createreferencerequest))
- **Success Response**:
  - Code: 200
  - Content: Integer (reference ID)
- **Error Response**:
  - Code: 404
  - Content: Error message if application is not found

### Create Form

- **URL**: `/skjema`
- **Method**: POST
- **Data Params**: JSON object (see [CreateFormRequest](#createformrequest))
- **Success Response**:
  - Code: 200
  - Content: Integer (form ID)
- **Error Response**:
  - Code: 400
  - Content: Error message if reference already has a form or if data is invalid

### Edit Form

- **URL**: `/skjema/{id}`
- **Method**: PUT
- **URL Params**:
  - `id` (integer, required): The form ID
- **Data Params**: JSON object (see [EditFormRequest](#editformrequest))
- **Success Response**:
  - Code: 200
  - Content: None
- **Error Response**:
  - Code: 400
  - Content: Error message if form is not found or if data is invalid

## Request/Response Formats

### CreateApplicationRequest

```json
{
  "DNumber": 12345678,
  "TravelDate": "2023-07-15",
  "Name": "John Doe"
}
```

### CreateReferenceRequest

```json
{
  "ApplicationId": 1234,
  "OrganisationNr": 987654321
}
```

### CreateFormRequest

```json
{
  "ReferenceId": 5678,
  "HasObjection": false,
  "SuggestedTravelDate": "2024-07-15",
  "HasDebt": false,
  "Email": "john.doe@example.com",
  "Phone": "+4712345678",
  "ContactName": "John Doe"
}
```

### EditFormRequest

```json
{
  "HasObjection": true,
  "SuggestedTravelDate": "2024-07-15",
  "HasDebt": false,
  "Email": "john.doe@example.com",
  "Phone": "+4712345678",
  "ContactName": "John Doe"
}
```

## Error Handling

The API uses standard HTTP response codes to indicate the success or failure of requests:

- 200: OK - The request was successful
- 400: Bad Request - The request was invalid, has invalid date format, or cannot be served
- 404: Not Found - The requested resource does not exist
- 500: Internal Server Error - The server encountered an unexpected condition

Error responses will include a message providing more details about the error. Specific validation includes:
- Date validation: Dates must be in "YYYY-MM-DD" format and must be in the future
- Duplicate checks: Cannot create multiple applications for the same DNumber
- Reference validation: Cannot create a form for a reference that already has one

## Rate Limiting

Rate limiting is not implemented in the current version.

## Examples

### Getting Reference Details

Request:
```http
GET /api/v1/referanse/1234 HTTP/1.1
Host: udi.azurewebsites.net
```

Response:
```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "ReferenceExists": true,
  "FormId": 5678,
  "TravelDate": "2023-07-15",
  "OrganisationNr": 987654321,
  "ApplicantName": "John Doe",
  "OrganisationName": "Example Organization AS",
  "Deadline": "2023-08-15T00:00:00"
}
```

### Creating a Form

Request:
```http
POST /api/v1/skjema HTTP/1.1
Host: udi.azurewebsites.net
Content-Type: application/json

{
  "ReferenceId": 5678,
  "HasObjection": false,
  "SuggestedTravelDate": "2024-07-15",
  "HasDebt": false,
  "Email": "john.doe@example.com",
  "Phone": "+4712345678",
  "ContactName": "John Doe"
}
```

Response:
```http
HTTP/1.1 200 OK
Content-Type: application/json

1234
```

This response indicates that a form was successfully created with ID 1234.
