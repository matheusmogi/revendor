export interface Customer {
    id: string;
    name: string;
    email: string;
    cpf: string;
    phoneNumbers?: {
        phoneNumber: string;
        label: string;
    }[];
    birthday?: string | null;
    address?: Address | null;
    notes?: string | null;
    tags: string[];
    new: boolean;
}

export interface Address {
    addressLine: string;
    streetNumber: string;
    complement: string;
    neighbourhood: string;
    city: string;
    state: string;
    zipCode: string;
}

export interface Country {
    id: string;
    iso: string;
    name: string;
    code: string;
    flagImagePos: string;
}

export interface Tag {
    id?: string;
    title?: string;
}
