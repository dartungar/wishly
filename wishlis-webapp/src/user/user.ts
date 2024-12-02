import {WishlistItem} from "../wishlist/wishlistItem";

export interface User {
  id: string;
  name: string;
  currencyCode: string;
  birthday: Date;
  isProfileSearchable: boolean;
}

export const createDefaultUser = (userId: string, birthday: Date | null, name: string | null): User => {
  return {
    id: userId,
    birthday: birthday ?? new Date("2000-01-01"),
    currencyCode: "USD",
    isProfileSearchable: true,
    name: name ?? "TEST USER"
  }
}
