const headers = (multpart: boolean = false) => {
  if (multpart) {
    return {
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'multipart/form-data',
      Authorization: 'Bearer ' + localStorage.getItem('token') || '',
    };
  } else {
    return {
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token') || '',
    };
  }
};

export default {
  headers,
};
