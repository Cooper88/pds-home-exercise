# UK Parliament - Person Manager

## Implementation Notes

### Responsive UI 

A responsive design has been implemented. 

### Frontend Validation 

Frontend validation has been implemented with the justification of giving user feedback regarding validation as soon as possible and not requiring an extra round trip to the server before presenting validation errors. 

To remove the frontend validation so the backend validation can be tested, comment out the validators in [editor.component.ts](https://github.com/Cooper88/pds-home-exercise/blob/main/UKParliament.CodeTest.Web/ClientApp/src/app/components/editor/editor.component.ts#L21)

### Backend Validation

Validation of form submission payload has been implemented by using ValidationAttributes. The custom validation attributes can be found under [UKParliament.CodeTest.Web/ValidationAttributes](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Web/ValidationAttributes).

One possible alternative approach to using ValidationAttributes is using [FluentValidation](https://docs.fluentvalidation.net/en/latest/aspnet.html) both 
approaches achieve keeping validation logic out of the services and in turn keeping the controllers as lightweight as possible. 


## Unit Testing

The [UKParliament.CodeTest.Tests](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests) project contains unit tests for the following:

- [Controllers](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests/Controllers)
- [Mapping](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests/Mapping)
- [Repositories](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests/Repositories) 
- [Services](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests/Services)
- [ValidationAttributes](https://github.com/Cooper88/pds-home-exercise/tree/main/UKParliament.CodeTest.Tests/ValidationAttributes)