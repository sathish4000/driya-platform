# Stage 1: Build the frontend application
FROM node:18-alpine AS build
WORKDIR /app

# Copy package.json and package-lock.json first (leveraging Docker caching)
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the source code into the container
COPY . .

# Build the frontend app for production
RUN npm run build

# Stage 2: Serve the frontend application using Nginx
FROM nginx:alpine AS production

# Copy the built application from the previous stage
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80 for the web server
EXPOSE 80

# Command to run Nginx
CMD ["nginx", "-g", "daemon off;"]
