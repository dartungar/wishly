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
    birthday: birthday ?? new Date("1900-01-01"),
    currencyCode: "USD",
    isProfileSearchable: true,
    name: name ?? "Display name"
  }
}
