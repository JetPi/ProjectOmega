/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { CompanyDTO } from "./company-dto";
import { UserRole } from "./user-role";

export interface UserDTO {
    id: string;
    companyId: string;
    company: CompanyDTO;
    username: string;
    password: string;
    email: string;
    role: UserRole;
    isOwner: boolean;
    createdAt: Date;
}
