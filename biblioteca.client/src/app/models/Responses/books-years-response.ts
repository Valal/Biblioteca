import { EntityBooksYears } from "../entities/entity-books-years"

export interface BooksYearsResponse {
  data: {
    type: string,
    attributes: Array<EntityBooksYears>
  }
}
