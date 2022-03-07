/* tslint:disable */
type ModelType =
  'USER' |
  'COMPANY' |
  'TOUR' |
  'CLIENT' |
  'LANGUAGE' |
  'OFFICE' |
  'TOUR_TIMING' |
  'TOUR_LOCATION' |
  'PORT' |
  'DOCUMENT' |
  'CONFIG' |
  'NOTE' |
  'BID' |
  'SERVICE' |
  'REGION' |
  'SHIP' |
  'REPORT' ;
module ModelType {
  export const USER: ModelType = 'USER';
  export const COMPANY: ModelType = 'COMPANY';
  export const TOUR: ModelType = 'TOUR';
  export const CLIENT: ModelType = 'CLIENT';
  export const LANGUAGE: ModelType = 'LANGUAGE';
  export const OFFICE: ModelType = 'OFFICE';
  export const TOUR_TIMING: ModelType = 'TOUR_TIMING';
  export const TOUR_LOCATION: ModelType = 'TOUR_LOCATION';
  export const PORT: ModelType = 'PORT';
  export const DOCUMENT: ModelType = 'DOCUMENT';
  export const CONFIG: ModelType = 'CONFIG';
  export const NOTE: ModelType = 'NOTE';
  export const BID: ModelType = 'BID';
  export const SERVICE: ModelType = 'SERVICE';
  export const REGION: ModelType = 'REGION';
  export const SHIP: ModelType = 'SHIP';
  export const REPORT: ModelType = 'REPORT';

  export function values(): ModelType[] {
    return [
      USER,
      COMPANY,
      TOUR,
      CLIENT,
      LANGUAGE,
      OFFICE,
      TOUR_TIMING,
      TOUR_LOCATION,
      PORT,
      DOCUMENT,
      CONFIG,
      NOTE,
      BID,
      SERVICE,
      REGION,
      SHIP,
      REPORT
    ];
  }
}

export {ModelType}


export const constants = {
  appName: 'Guides',
  cacheKey: 'app_cache',
  cacheMaxAge: 30000,
  defaultID: 0,
  securityStrategyName: 'app-security-strategy',
  dateFormat: 'dd/MM/yyyy',
  timeFormat: 'HH:mm',
  dateTimeFormat: 'dd/MM/yyyy HH:mm',
  apiDateTimeFormat: 'yyyy-MM-ddTHH:mm:ss',
  doubleFormat: '1.1-3',
  integerFormat: '1.0-0',
  googleMapsApiKey: 'AIzaSyDqMUiEVK8oBXwG1TWCB4hcRfXdKfwMgig',
};
