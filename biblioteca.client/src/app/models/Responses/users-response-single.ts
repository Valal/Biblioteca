import { EntityUser } from "../entities/entity-user"

export interface UsersResponseSingle {
  data: {
    type: string,
    attributes: EntityUser
  }
}
