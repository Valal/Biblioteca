export interface BooksResponseSingle {
  data: {
    type: string,
    attributes: {
      apa: string,
      name: string,
      lastNames: string,
      place: string,
      editorial: string,
      title: string,
      year: number,
      availables: number
    }
  }
}
