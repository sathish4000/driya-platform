# Use the latest stable version of Node.js
FROM node:latest

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json first (to leverage Docker cache for faster builds)
COPY package*.json ./ 

# Install dependencies
RUN npm install

# Copy the rest of the application files into the container
COPY . .

# Expose port 3000 for the Vue.js app
EXPOSE 3000

# Run the Vue.js development server
CMD ["npm", "run", "dev"]
