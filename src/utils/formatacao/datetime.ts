export const friendlyDateTime = (dateTimeString: string) => {
  const data = adjustStringDateForTimezone(dateTimeString);
  return `${data.toLocaleDateString()} às ${data
    .toLocaleTimeString()
    .substring(0, data.toLocaleTimeString().length - 3)}`;
};

export const brDateString = (date: string) => {
  return `${date.substring(8, 10)}/${date.substring(5, 7)}/${date.substring(0, 4)}`;
};

export const dateAndTimeToDatetime = (data: string, hora: string) => {
  return new Date(`${data}T${hora}`).toISOString();
};

export const adjustDateForTimezone = (date: Date) => {
  var timeOffsetInMS: number = date.getTimezoneOffset() * 60000;
  date.setTime(date.getTime() + timeOffsetInMS);
  return date;
};

export const adjustStringDateForTimezone = (date: string) => {
  const data = new Date(date);

  var timeOffsetInMS: number = data.getTimezoneOffset() * 60000;
  data.setTime(data.getTime() - timeOffsetInMS);
  return data;
};
