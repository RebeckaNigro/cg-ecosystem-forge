export const isPasswordValid = (password: string) => {
  var decimal = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$/;

  return password.match(decimal) ? true : false;
};
