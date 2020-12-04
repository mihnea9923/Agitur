import { TestBed } from '@angular/core/testing';

import { VocalMessageService } from './vocal-message.service';

describe('VocalMessageService', () => {
  let service: VocalMessageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VocalMessageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
