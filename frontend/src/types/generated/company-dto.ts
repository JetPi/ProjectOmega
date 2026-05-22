/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserDTO } from "./user-dto";

export interface CompanyDTO {
    id: string;
    name: string;
    email: string;
    phone: string;
    address: string;
    createdAt: Date;
    ownerId: string;
    owner: UserDTO;
    userIds: string[];
    users: UserDTO[];
}
