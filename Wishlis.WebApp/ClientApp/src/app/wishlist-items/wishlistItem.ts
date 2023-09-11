export interface WishlistItem {
  id: number;
  userId: number;
  name: string;
  url: string;
  cost: number;
  currency: number; // Enum?..
  isJointPurchase: boolean;
}
