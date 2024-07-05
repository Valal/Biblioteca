import { EntityUser } from "../entities/entity-user"

export interface UsersResponse {
  data: {
    type: string,
    attributes: Array<EntityUser>
  }
}
