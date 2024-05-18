export interface Apartment {
    id: any,
    name: string,
    description: string,
    address: string,
    price: number,
    cleaningFee: number,
    lastBookedOnUtc: Date,
    amenities : string[]
}
