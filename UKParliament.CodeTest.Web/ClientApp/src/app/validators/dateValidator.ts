import { AbstractControl, ValidationErrors } from '@angular/forms';

export function dateValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  if (!value) return null;

  // Check format YYYY-MM-DD
  const dateRegex = /^\d{4}-\d{2}-\d{2}$/;
  if (!dateRegex.test(value)) {
    return { invalidDate: 'Invalid format. Accepted format is YYYY-MM-DD.' };
  }

  // Check if it's a valid date
  const [year, month, day] = value.split('-').map(Number);
  const date = new Date(year, month - 1, day);

  if (
    date.getFullYear() !== year ||
    date.getMonth() + 1 !== month ||
    date.getDate() !== day
  ) {
    return { invalidDate: 'This date does not exist.' };
  }

  return null; // Valid date
}
