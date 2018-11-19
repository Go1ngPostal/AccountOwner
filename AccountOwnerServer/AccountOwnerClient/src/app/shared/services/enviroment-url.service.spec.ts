import { TestBed } from '@angular/core/testing';

import { EnviromentUrlService } from './enviroment-url.service';

describe('EnviromentUrlService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnviromentUrlService = TestBed.get(EnviromentUrlService);
    expect(service).toBeTruthy();
  });
});
