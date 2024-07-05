import { EntityUserBooks } from "../entities/entity-user-books";

export interface BooksByUserResponse {
  data: {
    type: string,
    attributes: Array<EntityUserBooks>
  }
}
