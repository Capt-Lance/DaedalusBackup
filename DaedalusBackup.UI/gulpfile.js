var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('scss', function () {
    return gulp.src('scss/*')
        .pipe(sass({
            includePaths: ['node_modules']
        }))
        .pipe(gulp.dest('wwwroot/css'))
});