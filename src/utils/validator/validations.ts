export const isPasswordValid = (password: string) => {
  return password.match(
    "/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^ws]).{8,}$/"
  )
    ? true
    : false
}
