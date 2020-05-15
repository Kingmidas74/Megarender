import { TestBed } from '@angular/core/testing';

import { MegarenderApiService } from './megarender-api.service';

describe('MegarenderApiService', () => {
  let service: MegarenderApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MegarenderApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
