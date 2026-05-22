/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { CompanyDTO } from "./company-dto";

export interface UserDTO {
    id: string;
    companyId: string;
    company: CompanyDTO;
    username: string;
    email: string;
    role: string;
    isOwner: boolean;
    createdAt: Date;
}
