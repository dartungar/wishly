
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
    currencyCode: "USD", // TODO: set currency based on user's settings. Or better yet use the user's settings instead of item field
    isGroupGift: false
  }
}
