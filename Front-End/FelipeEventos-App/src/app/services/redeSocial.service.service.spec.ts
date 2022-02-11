/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RedeSocial.serviceService } from './redeSocial.service.service';

describe('Service: RedeSocial.service', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RedeSocial.serviceService]
    });
  });

  it('should ...', inject([RedeSocial.serviceService], (service: RedeSocial.serviceService) => {
    expect(service).toBeTruthy();
  }));
});
