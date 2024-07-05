import { EntityBooks } from "../entities/entity-books"

export interface BooksResponse {
  data: {
    type: string,
    attributes: EntityBooks[]
  }
}
