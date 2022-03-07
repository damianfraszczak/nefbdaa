import {DatePipe} from '@angular/common';
import {constants} from '../shared-constants';

export interface Month {
  number: number;
  name: string;
}

export function* monthsGenerator(initialDate = new Date()) {
  let month = initialDate.getMonth();
  while (true) {
    const iterationDiff = (yield month) || 1;
    month = (month + iterationDiff + 12) % 12;
  }
}

export const monthNames = [
  'January', 'February', 'March', 'April', 'May', 'June',
  'July', 'August', 'September', 'October', 'November', 'December',
];

export function monthsBackwards(take: number, initialDate = new Date()): Month[] {
  return months(take, initialDate, -1);
}

export function months(take: number, initialDate = new Date(), iterationDiff: -1 | 1 = 1): Month[] {
  const generator = monthsGenerator(initialDate);
  return Array.from({length: take}, () => generator.next(iterationDiff).value)
    .map(i => ({number: i, name: monthNames[i]}));

}

export function setDay(date: Date, day: number): Date {
  return new Date(date.getFullYear(), date.getMonth(), day);
}

export function getLastDayOfMonthOfDate(date: Date): number {
  return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDay();
}

export function formatDate(date: Date): string {
  return new DatePipe(constants.defaultLocale).transform(date, constants.dateFormat);
}

export function formatDateTime(date: Date): string {
  return new DatePipe(constants.defaultLocale).transform(date, constants.dateTimeFormat);
}

export function formatTime(date: Date): string {
  return new DatePipe(constants.defaultLocale).transform(date, constants.timeFormat);
}

export function formatDateTimeForApi(date: Date): string {
  return new DatePipe(constants.defaultLocale).transform(new Date(date), constants.apiDateTimeFormat);
}

export function isDateString(date): boolean {
  if (!isNaN(Date.parse(date))) {
    return true;
  } else {
    return false;
  }
}

export function getDate(date): Date {
  if (date && date instanceof Date) {
    return date;
  }
  if (isDateString(date)) {
    return new Date(date);
  }
  return null;
}
export function getDateWithoutTime(date): Date {
  if (date && date instanceof Date) {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }
  return null;
}
/**
 * Compares two Date objects and returns e number value that represents
 * the result:
 * 0 if the two dates are equal.
 * 1 if the first date is greater than second.
 * -1 if the first date is less than second.
 * @param date1 First date object to compare.
 * @param date2 Second date object to compare.
 */
export function compareDate(date1: Date, date2: Date): number {
  // With Date object we can compare dates them using the >, <, <= or >=.
  // The ==, !=, ===, and !== operators require to use date.getTime(),
  // so we need to create a new instance of Date with 'new Date()'
  const d1 = new Date(date1);
  const d2 = new Date(date2);

  // Check if the dates are equal
  const same = d1.getTime() === d2.getTime();
  if (same) {
    return 0;
  }
  // Check if the first is greater than second
  if (d1 > d2) {
    return 1;
  }
  // Check if the first is less than second
  if (d1 < d2) {
    return -1;
  }
}
