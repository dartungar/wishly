
export interface WishlistItem {
  id: string;
  userId: string;
  name: string;
  price: number;
  currencyCode: string;
  url: string;
  isGroupGift: boolean;
}

export const createDefaultWishlistItem = (userId: string, currencyCode: string | undefined): WishlistItem => {
  return {
    id: crypto.randomUUID(),
    userId,
    name: "New Item",
    url: "",
    price: 0,
    currencyCode: currencyCode ?? "USD",
    isGroupGift: false
  }
}
