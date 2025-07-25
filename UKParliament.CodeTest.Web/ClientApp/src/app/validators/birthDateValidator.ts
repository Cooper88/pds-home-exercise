import {AbstractControl, ValidationErrors} from '@angular/forms';

export function birthDateValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  if (!value) return null;

  // Check format DD/MM/YYYY
  const dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
  if (!dateRegex.test(value)) {
    return {invalidDate: 'Invalid format. Accepted format is DD/MM/YYYY.'};
  }

  // Check if it's a valid date
  const [day, month, year] = value.split('/').map(Number);
  const date = new Date(year, month - 1, day);

  if (
    date.getFullYear() !== year ||
    date.getMonth() + 1 !== month ||
    date.getDate() !== day
  ) {
    return {invalidDate: 'This date is invalid.'};
  }

  if (date > new Date()) {
    return {invalidDate: 'Date is in the future.'};
  }

  return null;
}
