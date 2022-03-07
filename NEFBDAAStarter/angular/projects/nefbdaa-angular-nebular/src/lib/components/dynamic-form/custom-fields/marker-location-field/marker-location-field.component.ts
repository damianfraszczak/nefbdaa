import {Component, forwardRef} from '@angular/core';
import {NG_VALUE_ACCESSOR} from '@angular/forms';
import {GeolocationModel} from '../../../../models/entities/geolocation-model';
import {AbstractField} from '../abstract-field';

@Component({
  selector: 'ngx-marker-location-field',
  templateUrl: './marker-location-field.component.html',
  styleUrls: ['./marker-location-field.component.scss'],
  providers: [
    {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => MarkerLocationFieldComponent), multi: true},
  ],
})
export class MarkerLocationFieldComponent extends AbstractField<GeolocationModel> {
  mapProp = {
    lat: 9.234960,
    lon: 20.357078,
    zoom: 3,
    mapTypeId: 'hybrid',
    mapTypeControl: true,
    streetViewControl: false,
    fullscreenControl: true,
    scaleControl: true,
  };
  constructor() {
    super();
    this.value = {lng: 20.357078, lat: 9.234960};
  }

  dragEnd(coords: { lat, lng }) {
    if (this.value == null) {
      this.value = new GeolocationModel();
    }
    this.value.lat = coords.lat;
    this.value.lng = coords.lng;
  }


}
