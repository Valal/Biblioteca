import { EntityError } from "../entities/entity-error";

export interface ErrorResponse {
  errors: Array<EntityError>
}
