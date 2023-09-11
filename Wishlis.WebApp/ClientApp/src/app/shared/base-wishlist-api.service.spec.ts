import { TestBed } from '@angular/core/testing';

import { WishlistApiClient } from './wishlist-api-client.service';

describe('BaseWishlistApiService', () => {
  let service: WishlistApiClient;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WishlistApiClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
