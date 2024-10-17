
export interface WishlistItem {
  id: string;
  userId: string;
  name: string;
  price: number;
  currencyCode: string;
  url: string;
  isGroupGift: boolean;
}

export const createDefaultWishlistItem = (userId: string): WishlistItem => {
  return {
    id: crypto.randomUUID(),
    userId,
    name: "New Item",
    url: "",
    price: 0,
    currencyCode: "",
    isGroupGift: false
  }
}
