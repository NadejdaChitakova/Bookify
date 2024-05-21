import { Address } from "./address";

export interface Apartment {
    id: any,
    name: string,
    description: string,
    address: Address,
    price: number,
    cleaningFee: number,
    lastBookedOnUtc: Date,
    amenities : string[]
}
