# UK Parliament - Person Manager

## Implementation Notes

### Frontend Validation 

Frontend validation has been implemented. 

### Backend Validation

Validation of form submission payload has been implemented by using ValidationAttributes. The custom validation attributes can be found under UKParliament.CodeTest.Web/ValidationAttributes.

One possible alternative approach to using ValidationAttributes is using [FluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html) both 
approaches achieve keeping validation logic out of the services and in turn keeping the controllers as lightweight as possible. 


## Unit Testing

The UKParliament.CodeTests.Tests project contains unit tests for the following:

- Controllers
- Mapping
- Repositories 
- Services
- ValidationAttributes 