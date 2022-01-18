import { TestBed } from '@angular/core/testing';

import { SpotfiyAuthenticationService } from './spotfiy-authentication.service';

describe('SpotfiyAuthenticationService', () => {
  let service: SpotfiyAuthenticationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SpotfiyAuthenticationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
